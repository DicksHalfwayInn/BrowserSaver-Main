using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserLibrary
{
    /// <summary>
    /// Represents a personal tag that I would associate
    /// with a webpage.  gets added to the list of tags for
    /// a website for use in searching, sorting, and filtering
    /// </summary>
    public class TagModel
    {
        public string Tagline { get; set; }

        public TagModel() : this(null)
        {
          
        }

        public TagModel(string _tagline)
        {
            Tagline = _tagline;
        }
    }
}
