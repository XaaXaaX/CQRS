namespace SeLoger.Api.Streaming
{
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Configuration;
    using SeLoger.WebApi.Toolkit.Net.Core.Extensions;
    using SeLoger.WebApi.Toolkit.Net.Core.Filters;
    using SeLoger.WebApi.Toolkit.Net.Core.Middlewares;
    using Swashbuckle.AspNetCore.Swagger;
    using System.Linq;
    using SeLoger.Framework.Standard.Log4NetExtensions;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using SeLoger.WebApi.Toolkit.Net.Core.Models;

    internal class Startup
    {
        private WebApiConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration.GetSection("WebApiConfiguration").Get<WebApiConfiguration>();

            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration.HttpHeaders);
            services.AddSingleton(Configuration.MonitoringHeader);

            services.AddLogging();

            services
                .AddMvcCore()
                .AddMvcOptions(options => options.OutputFormatters.RemoveType<StringOutputFormatter>())
                .AddMvcOptions(options => options.RespectBrowserAcceptHeader = true)
                .AddMvcOptions(options => options.ReturnHttpNotAcceptable = true)
                .AddMvcOptions(options => options.CatchExceptions(StatusCodes.Status500InternalServerError, x => x.Message))
                .AddMvcOptions(options => options.CheckModelState(x => x.ToResultModel(2).BadRequest()))
                .AddMvcOptions(options => options.Filters.Add<MonitoringHeaderFilter>())
                .AddVersionedApiExplorer(options =>
                {
                    // configuration du group format https://github.com/Microsoft/aspnet-api-versioning/wiki/Version-Format#custom-api-version-format-strings
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                })
                .AddDataAnnotations()
                .AddJsonFormatters(settings => settings.Formatting = Formatting.Indented)
                .AddApiExplorer()
                .AddCors()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SampleValidator>());

            if (Configuration.SwaggerConfig.IsEnabled)
            {
                services.AddApiVersioning(options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = ApiVersion.Default;
                    options.ReportApiVersions = true;
                });

                services.AddSwaggerGen(options =>
                {
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                    }

                    options.OperationFilter<TrackingIdFilter>(this.Configuration.HttpHeaders?.TrackingIdHeaderName);
                    options.OperationFilter<CorrelationIdFilter>(this.Configuration.HttpHeaders?.CorrelationIdHeaderName);
                    options.OperationFilter<UploadFileFilter>();
                    options.AddXmlCommentsFiles(Configuration.SwaggerConfig?.XmlComments);
                    options.AddFluentValidationRules();
                });
            }
        }

        private Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info
            {
                Title = $"{Configuration.SwaggerConfig.Title} {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = Configuration.SwaggerConfig.Description,
                Contact = Configuration.SwaggerConfig.Contact,
                TermsOfService = Configuration.SwaggerConfig.TermsOfService,
                License = Configuration.SwaggerConfig.License
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.UseLog4Net();

            //  enable tracking id
            app.UseTrackingId(this.Configuration.HttpHeaders.TrackingIdHeaderName);

            //  enable correlation id
            app.UseCorrelationId(this.Configuration.HttpHeaders.CorrelationIdHeaderName);

            //  enable ip filtering
            //app.UseSwaggerIpFiltering("Enter ip and ip-range here");

            //  configure cross domain
            app.UseCors(
                builder => builder
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://www.seloger.com", "http://www.selogerneuf.com")
            );

            if (Configuration.SwaggerConfig.IsEnabled)
            {
                //  configure swagger
                app.UseSwagger();

                //  configure swagger ui            
                app.UseSwaggerUI(
                    options =>
                    {
                        options.DocumentTitle = Configuration.SwaggerConfig.Title;

                        var provider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                        foreach (var apiVersionDescription in provider
                            .ApiVersionDescriptions
                            .OrderByDescending(x => x.ApiVersion))
                        {
                            options.SwaggerEndpoint(
                                $"{apiVersionDescription.GroupName}/swagger.json",
                                $"Version {apiVersionDescription.ApiVersion}");
                        }
                    });

                //  enable swagger root redirection ( / -> /swagger )
                app.UseSwaggerRootRedirection();

            }

            //app.UseHsts();
            //app.UseHttpsRedirection();

            //  configure mvc
            app.UseMvc();
        }
    }
}
