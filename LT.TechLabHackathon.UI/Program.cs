using Blazored.LocalStorage;
using LT.TechLabHackathon.UI;
using LT.TechLabHackathon.UI.Core;
using LT.TechLabHackathon.UI.DataAccess.Contracts;
using LT.TechLabHackathon.UI.DataAccess.Repositories;
using LT.TechLabHackathon.UI.Providers.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient("api-techlab-hackathon", client => client.BaseAddress = new Uri("https://localhost:7098"));
//builder.Services.AddHttpClient("api-techlab-hackathon", client => client.BaseAddress = new Uri("https://techlab-hackathon.azurewebsites.net"));

builder.Services.AddSingleton(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("api-techlab-hackathon"));

builder.Services.AddScoped<IProgrammingLanguageAPIRepository, ProgrammingLanguageAPIRepository>();
builder.Services.AddScoped<IChallengeLevelAPIRepository, ChallengeLevelAPIRepository>();
builder.Services.AddScoped<IChallengeAPIRepository, ChallengeAPIRepository>();
builder.Services.AddScoped<IUserAPIRepository, UserAPIRepository>();
builder.Services.AddScoped<IAuthenticationAPIRepository, AuthenticationAPIRepository>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStateJWT>(options=>options.GetRequiredService<AuthenticationStateJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationStateJWT>(options => options.GetRequiredService<AuthenticationStateJWT>());

builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);

var collectionShared = new CollectionsShared();
builder.Services.AddSingleton(collectionShared);

await builder.Build().RunAsync();
