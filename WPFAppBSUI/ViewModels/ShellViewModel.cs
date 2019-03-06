using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using BrowserLibrary;
using Caliburn.Micro;

namespace WPFAppBSUI.ViewModels
{
    public class ShellViewModel : Screen
    {

        #region               Public Properties with Private members

        private int mWidthOfCurrentRecordsDataGrid;
        public int WidthOfCurrentRecordsDataGrid
        {
            get
            {
                return mWidthOfCurrentRecordsDataGrid;
            }
            set
            {
                mWidthOfCurrentRecordsDataGrid = value;
                NotifyOfPropertyChange(() => WidthOfCurrentRecordsDataGrid);
            }
        }

        /// <summary>
        /// Expands the webpage datagrid if true 
        /// - sets the Width of the datagrid to 400
        /// </summary>
        private bool mWebPageDGExpanded;
        public bool WebPageDGExpanded
        {
            get
            {
                return mWebPageDGExpanded;
            }
            set
            {
                mWebPageDGExpanded = value;
                NotifyOfPropertyChange(() => WebPageDGExpanded);
                WidthOfCurrentRecordsDataGrid = 400;

            }
        }

        
        /// <summary>
        /// Sets the Address of the added web page
        /// </summary>
        private string mAddedWebPageAddress;
        public string AddedWebPageAddress
        {
            get
            {
                return mAddedWebPageAddress;
            }
            set
            {
                mAddedWebPageAddress = value;
                NotifyOfPropertyChange(() => AddedWebPageAddress);
            }
        }

        /// <summary>
        /// Sets the Description of the added web page
        /// </summary>
        private string mAddedWebPageDescription;
        public string AddedWebPageDescription
        {
            get
            {
                return mAddedWebPageDescription;
            }
            set
            {
                mAddedWebPageDescription = value;
                NotifyOfPropertyChange(() => AddedWebPageDescription);
            }
        }

        /// <summary>
        /// Sets the List of Tags
        /// </summary>
        private List<TagModel> mAddedWebPageTags;
        public List<TagModel> AddedWebPageTags
        {
            get
            {
                return mAddedWebPageTags;
            }
            set
            {
                mAddedWebPageTags = value;
                NotifyOfPropertyChange(() => AddedWebPageTags);


            }
        }

        /// <summary>
        ///                CurrentWebPages - <BindableCollection<WebPage>>
        /// - Represents the list of WebPages read from the file
        /// 
        /// </summary>
        private BindableCollection<WepPageModel> mCurrentWebPages = new BindableCollection<WepPageModel>();
        public BindableCollection<WepPageModel> CurrentWebPages
        {
            get
            {
                return mCurrentWebPages;
            }
            set
            {
                mCurrentWebPages = value;
                NotifyOfPropertyChange(() => CurrentWebPages);
            }
        }

        /// <summary>
        ///                SelectedWebPage - <WebPageModel>
        /// - Represents the selected web page in the WebPage DataGrid
        /// - When a webpage is selected in the datagrid it updates the SelectedWebPage
        /// property, which in turn updates the Tags datagrid with the SelectedWebPage's
        /// Tags
        ///                notify : self
        ///                notify : SelectedWebPage
        /// </summary>
        private WepPageModel mSelectedWebPage;
        public WepPageModel SelectedWebPage
        {
            get { return mSelectedWebPage; }
            set
            {
                if (value != null)
                {
                    mSelectedWebPage = value;
                    BindableCollection<TagModel> tags = new BindableCollection<TagModel>();
                    if (value != null)
                    {
                        if (value.Tags != null)
                        {
                            foreach (TagModel tag in value.Tags)
                            {
                                tags.Add(tag);
                            }
                            SelectedWebPageTags = tags;
                        }
                    }
                    WebPageDGExpanded = true;
                    SelectedWebPageAddress = value.Address;
                    NotifyOfPropertyChange(() => SelectedWebPageTags);
                    NotifyOfPropertyChange(() => SelectedWebPage);
                }
                

            }
        }

        private string mSelectedWebPageAddress = "https://www.google.com/";
        public string SelectedWebPageAddress
        {
            get
            {
                return mSelectedWebPageAddress;
            }
            set
            {
                mSelectedWebPageAddress = value;
                NotifyOfPropertyChange(() => SelectedWebPageAddress);
                
            }
        }

        /// <summary>
        ///               SelectedWebPageTags - <BindableCollection<TagModel>>
        /// - Represents the Tags associated with the selectedWebPage
        ///               Notify : self
        /// </summary>
        private BindableCollection<TagModel> mSelectedWebPageTags;
        public BindableCollection<TagModel> SelectedWebPageTags
        {
            get
            {
                return mSelectedWebPageTags;
            }
            set
            {
                mSelectedWebPageTags = value;
                NotifyOfPropertyChange(() => SelectedWebPageTags);
            }

        }

        /// <summary>
        ///                  SelectedTag - <TagModel>
        /// - Represents the selected tag in the selected WebPage associated list of tags    
        ///                  Notify : Self
        /// </summary>
        private TagModel mSelectedTag;
        public TagModel SelectedTag
        {
            get
            {
                return mSelectedTag;
            }
            set
            {
                mSelectedTag = value;
                NotifyOfPropertyChange(() => SelectedTag);
            }
        }

        private bool mTextBoxVisible;
        public bool TextBoxVisible
        {
            get
            {
                return mTextBoxVisible;
            }
            set
            {
                mTextBoxVisible = value;
                NotifyOfPropertyChange(() => TextBoxVisible);
            }
        }

        //private string mInnerText;
        //public string InnerText
        //{
        //    get
        //    {
        //        return mInnerText;
        //    }
        //    set
        //    {
        //        mInnerText = value;
        //        NotifyOfPropertyChange(() => InnerText);
        //    }
        //}
        #endregion

        //public event PropertyChangedEventHandler PropertyChanged;

        //public void RaisePropertyChanged(string propertyName)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}

        #region               Constructors
        /// <summary>
        ///                Default Constructor
        /// </summary>
        public ShellViewModel()
        {
            var rrda = new RayRabbitDataAccess();
            rrda.SetFileNameToGenericName<WepPageModel>();
            rrda.SetPathToSolutionPath();
            var receivedRecords = rrda.GetObjectsFromXMLFile<List<WepPageModel>>();
            foreach (WepPageModel webPage in receivedRecords)
            {
                CurrentWebPages.Add(webPage);
            }
            TextBoxVisible = false;
            WebPageDGExpanded = false;
            AddedWebPageTags = new List<TagModel>();
            //var tempWebPageTag = new TagModel();
            //tempWebPageTag.Tagline = "TagIsh linish";
            //AddedWebPageTags.Add(tempWebPageTag);
            SelectedWebPageTags = new BindableCollection<TagModel>();
            
        }
        #endregion

        public bool CanBut()
        {
            return true;
        }

        public void But()
        {
            //for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            //    if (vis is DataGridRow)
            //    {
            //        var row = (DataGridRow)vis;
            //        row.DetailsVisibility =
            //        row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            //        break;
            //    }
        }

        // Delete this stuff
        public void AddWebPageButton()
        {
            TextBoxVisible = true;
        }
        public void DontAddWebPageButton()
        {
            TextBoxVisible = false;
        }

        public void SaveNewWebpage()
        {
            var newWebPage = new WepPageModel();

            newWebPage.Address = AddedWebPageAddress;
            newWebPage.Description = AddedWebPageDescription;
           
            foreach (TagModel tag in AddedWebPageTags)
            {
                newWebPage.Tags.Add(tag);
            }
            

            var rayCarrotDataAccess = new RayRabbitDataAccess();
            rayCarrotDataAccess.SetFileNameToGenericName<WepPageModel>();
            rayCarrotDataAccess.SetPathToSolutionPath();
            // this needs to be a seperate method to add the new web page
            //CurrentWebPages.Add(newWebPage);
            
            List<WepPageModel> WebPageToSaveList = new List<WepPageModel>();
            WebPageToSaveList.Add(newWebPage);
            //WebPageToSaveList = ConvertBindableCollectionToList<WepPageModel>(CurrentWebPages);
            rayCarrotDataAccess.SaveObjectsToXMLFile<List<WepPageModel>, WepPageModel>(WebPageToSaveList);
            SetTheAddedInfoBoxesToBlank();
            TextBoxVisible = false;
            CurrentWebPages = new BindableCollection<WepPageModel>();
            var receivedRecords = rayCarrotDataAccess.GetObjectsFromXMLFile<List<WepPageModel>>();
            foreach (WepPageModel webPage in receivedRecords)
            {
                CurrentWebPages.Add(webPage);
            }
        }

        public List<T> ConvertBindableCollectionToList<T>(BindableCollection<T> bindableCollection)
        {
            var newList = new List<T>();
            foreach (T j in bindableCollection)
            {
                newList.Add(j);
            }
            return newList;
        }

        public void SetTheAddedInfoBoxesToBlank()
        {

            AddedWebPageAddress = null;

            AddedWebPageDescription = null;

            AddedWebPageTags = new List<TagModel>();

        }
    }
}
