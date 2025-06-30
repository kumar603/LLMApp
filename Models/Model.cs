using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LLM_Interaction.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProviderId { get; set; }

        public virtual Provider Provider { get; set; }
        public virtual ICollection<Prompt> Prompts { get; set; }
    }
}