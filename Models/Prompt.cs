using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LLM_Interaction.Models
{
    public class Prompt
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public int ModelId { get; set; }

        public virtual Model Model { get; set; }
    }
}