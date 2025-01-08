using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ResearchWebApp.Services;
using ResearchWebApp.Factories;
using ResearchWebApp.Components;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using Microsoft.AspNetCore.Http.Features;
using PdftoImageApp.Service;
using ResearchWebApp.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient();
builder.Services.AddHttpClient<OpenAIService>();
builder.Services.AddScoped<IPdfConversionService, PdfConversionService>();
builder.Services.AddScoped<IOcrService, OcrService>();
builder.Services.AddScoped<ITextCombinerService, TextCombinerService>();
builder.Services.AddScoped<SubjectStateService>();
builder.Services.AddScoped<IRelatedLiteratureService, RelatedLiteratureService>();

builder.Services.AddBlazorBootstrap();

builder.Services.AddHttpClient<GoogleCustomSearchService>();
builder.Services.AddScoped<GoogleCustomSearchService>();




//File upload size
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 100 * 1024 * 1024; // 100 MB
});

// Register Syncfusion license key
SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWX5ed3RXQmlZVkF2XUs=");

builder.Services.AddSyncfusionBlazor();
builder.Services.AddSingleton<ISyncfusionStringLocalizer, CustomSyncfusionStringLocalizer>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the IDbContextFactory<DataContext> as scoped
builder.Services.AddScoped<IDbContextFactory<DataContext>, DbContextFactory<DataContext>>();

builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IExamScheduleService, ExamScheduleService>();
builder.Services.AddScoped<IStudySessionService, StudySessionService>();
builder.Services.AddScoped<ISubjectFileService, SubjectFileService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuizQuestionService, QuizQuestionService>();
builder.Services.AddScoped<IQuizAttemptService, QuizAttemptService>();
builder.Services.AddScoped<IQuizAnswerService, QuizAnswerService>();

string reviewerFolderPath = Path.Combine(builder.Environment.ContentRootPath, "Reviewer");







builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();