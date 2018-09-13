using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KenticoOnboardingApplication.Api.Models
{
    public class Item
    {
        public string Text { get; }
        public bool IsEdited { get; set;  } = false;

        public Item(string text)
        {
            Text = text;
        }
    }
}