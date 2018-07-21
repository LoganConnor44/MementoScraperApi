using System;
using System.Collections.Generic;
using System.Text;

namespace MementoScraperApi.Models {
    /// <summary>
    /// A class to define a Media object that can originate from any social media platform.
    /// </summary>
    public class Memory {

        public long? MemoryId { get; set; }

        public int Id { get; set; }

        public int MementoId { get; set; }
        public Memento Memento { get; set; }

        /// <summary>
        /// The direct url for a media entity.
        /// </summary>
        /// <value></value>
        public string MediaURL { get; set; }

        /// <summary>
        /// A security direct url for a media entity.
        /// </summary>
        /// <value></value>
        public string MediaURLHttps { get; set; }

        /// <summary>
        /// A url for the media entity.
        /// 
        /// May need to eliminate this and only use mediaURL
        /// </summary>
        /// <value></value>
        public string Url { get; set; }

        /// <summary>
        /// A url for the Memento.
        /// </summary>
        /// <value></value>
        public string DisplayURL { get; set; }

        /// <summary>
        /// A url for a long url.
        /// </summary>
        /// <value></value>
        public string ExpandedURL { get; set; }

        /// <summary>
        /// The type of media stored.
        /// </summary>
        /// <value></value>
        public string MediaType { get; set; }

        /// <summary>
        /// Date of found Memory.
        /// </summary>
        /// <value></value>
        public DateTime Creation { get; set; }
    }
}