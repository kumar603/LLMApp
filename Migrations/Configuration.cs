namespace LLM_Interaction.Migrations
{
    using LLM_Interaction.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LLM_Interaction.Data.LLMDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LLM_Interaction.Data.LLMDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            // Seed Providers
            var openAI = new Provider { Name = "OpenAI", ApiUrl = "https://api.openai.com", ApiKey = "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" };
            var azure = new Provider { Name = "Azure OpenAI", ApiUrl = "https://azure.openai.com", ApiKey = "azureopenapi-key" };

            var microsoftCopilot = new Provider { Name = "Microsoft Copilot", ApiUrl = "https://microsoft.copilot.com", ApiKey = "microsoftapi-key" };

            //context.Providers.AddOrUpdate(p => p.Name, openAI, azure);
            context.Providers.AddOrUpdate(p => p.Name, openAI, azure, microsoftCopilot);
            context.SaveChanges();

            // Seed Models
            //var gpt4 = new Model { Name = "GPT-4", ProviderId = openAI.Id };
            var gpt35 = new Model { Name = "GPT-3.5", ProviderId = openAI.Id };

            //context.Models.AddOrUpdate(m => m.Name, gpt4, gpt35);
            context.Models.AddOrUpdate(m => m.Name, gpt35);
            context.SaveChanges();

            // Seed Prompts
            var prompt = new Prompt
            {
                Title = "Explain Dependency Injection",
                Content = "Explain DI in ASP.NET MVC with an example.",
                Tags = "ASP.NET,DI",
                ModelId = gpt35.Id
            };

            context.Prompts.AddOrUpdate(p => p.Title, prompt);
            context.SaveChanges();
        }

    }
}
