using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserLibrary
{
    /// <summary>
    /// Model for a web page
    /// </summary>
    public class WepPageModel
    {
        #region Public Properties

        
        /// <summary>
        /// The address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The tags
        /// </summary>
        public HashSet<TagModel> Tags { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WepPageModel() : this(null)
        {

        }

        /// <summary>
        /// Constructor with an address
        /// </summary>
        /// <param name="address">The address</param>
        public WepPageModel(string address) : this(address, null, null)
        {

        }

        /// <summary>
        /// Constructor with an address, description and tags
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="description">The description</param>
        /// <param name="tags">The tags</param>
        public WepPageModel(string address, string description, IEnumerable<TagModel> tags)
        {
            Address = address;
            Description = description;

            // This can be simplified into:
            // Tags = new List<TagModel>(tags);
            // Also, does the list need to be ordered? If the tag order
            // doesn't matter you should use a HashSet instead.
            if (!(tags == null))
            {
                Tags = new HashSet<TagModel>(tags);
            }
            else Tags = new HashSet<TagModel>();
        }

        #endregion

    }
}

//    {
//        #region Public Properties

//        /// <summary>
//        /// The address
//        /// </summary>
//        public string Address { get; set; }

//    /// <summary>
//    /// The description
//    /// </summary>
//    public string Description { get; set; }

//    /// <summary>
//    /// The tags
//    /// </summary>
//    // You should remove the setter here and just do:
//    // public List<TagModel> Tags { get; }
//    // since you'll never replace the list, just modify its content
//    // Doing just a getter makes it readonly which means it can only be
//    // set up once during construction.
//    // You can do the same for the address and description if they will never change, but
//    // you might want to allow those to be edited
//    public List<TagModel> Tags { get; set; }

//    #endregion

//    #region Constructors

//    /// <summary>
//    /// Default constructor
//    /// </summary>
//    public WepPageModel() :
//        // This calls the second constructor
//        this(null)
//    {

//    }

//    /// <summary>
//    /// Constructor with an address
//    /// </summary>
//    /// <param name="address">The address</param>
//    public WepPageModel(string address) :
//        // This calls the third constructor to avoid duplicating code
//        this(address, null,
//            // Take a look at my comment in the third constructor regarding using IEnumerable<TagModel>
//            // If you do that you will have the benefit of being able to use an array here instead of creating an empty
//            // list which is more memory efficient
//            // This would then be:
//            // new TagModel[0]
//            new List<TagModel>())
//    {

//    }

//    /// <summary>
//    /// Constructor with an address, description and tags
//    /// </summary>
//    /// <param name="address">The address</param>
//    /// <param name="description">The description</param>
//    /// <param name="tags">The tags</param>
//    public WepPageModel(string address, string description,
//        // This should be an IEnumerable<TagModel> instead of a list
//        // The reason is that there is no reason to force it to be a list in this case
//        // since you're just adding it to your own list. Having it just be a generic
//        // interface allows it to be a collection of any type. If you look at the List.AddRange
//        // method you can see that it takes in an IEnumerable<T>
//        List<TagModel> tags)
//    {
//        Address = address;
//        Description = description;

//        // This can be simplified into:
//        // Tags = new List<TagModel>(tags);
//        // Also, does the list need to be ordered? If the tag order
//        // doesn't matter you should use a HashSet instead.
//        Tags = new List<TagModel>();
//        Tags.AddRange(tags);
//    }

//    #endregion

//}

