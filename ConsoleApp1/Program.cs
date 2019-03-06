using BrowserLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool mQuitProgram = false;
            List<WepPageModel> mPages = new List<WepPageModel>();

            while (!mQuitProgram)
            {
                Console.WriteLine("Enter Web address ('END' or 'return' to quit): ");
                string mWebAddress = Console.ReadLine();

                mQuitProgram = mWebAddress.Equals("END", StringComparison.CurrentCultureIgnoreCase);
                if (!mQuitProgram) { mQuitProgram = mWebAddress.Equals(""); }
                if (!mQuitProgram)
                {
                    bool mQuitEnteringTags = false;

                    WepPageModel PageModel = new WepPageModel(mWebAddress);

                    while (!mQuitEnteringTags)
                    {
                        // initialize loop variables
                        int i = 1; bool emptyTag = false;
                        // loop
                        do
                        {
                            Console.WriteLine($"Enter Tag {i}: ");
                            TagModel tag = new TagModel(Console.ReadLine());

                            emptyTag = (string.IsNullOrEmpty(tag.Tagline));

                            if (!emptyTag)
                            {
                                PageModel.Tags.Add(tag);

                                i++;
                            }

                        } while (!emptyTag);

                        mQuitEnteringTags = true;

                    }

                    mPages.Add(PageModel);

                }
            }

            Console.WriteLine("finished entering webpages");

            var rayCarrotDataAccess = new RayRabbitDataAccess();
            rayCarrotDataAccess.SetFileNameToGenericName<WepPageModel>();
            rayCarrotDataAccess.SetPathToSolutionPath();
            rayCarrotDataAccess.SaveObjectsToXMLFile<List<WepPageModel>, WepPageModel>(mPages);
            
            List<WepPageModel> receivedPages = rayCarrotDataAccess.GetObjectsFromXMLFile<List<WepPageModel>>();



        }
    }
}