using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LLM_Interaction.Models;
namespace LLM_Interaction.Data
{
    public class LLMDbContext : DbContext
    {
            public LLMDbContext() : base("name=LLMDbContext")
            {
            }
            public DbSet<Provider> Providers { get; set; }
            public DbSet<Model> Models { get; set; }
            public DbSet<Prompt> Prompts { get; set; }
         
    }
}