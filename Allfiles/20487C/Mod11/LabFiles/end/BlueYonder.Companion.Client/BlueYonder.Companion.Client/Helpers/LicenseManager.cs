using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store;

namespace BlueYonder.Companion.Client.Helpers
{
    public class LicenseManager
    {

		private ListingInformation _listingInformation;
		
		//Singleton Implementations
        private static LicenseManager _instance;
        public static LicenseManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LicenseManager();
                }
                return _instance;
            }
        }

		public bool IsTrialLicense { get; private set; }
        
        public event EventHandler LicenseDataUpdated;

        public bool IsMediaFeatureEnabled { get; private set; }

        
        private LicenseManager()
        {
            CurrentAppSimulator.LicenseInformation.LicenseChanged += LicenseInformation_LicenseChanged;
        }


        private bool _loaded = false;
        public async Task LoadLicenseData()
        {
            if (_loaded)
                return;
            var installedFolder = await Package.Current.InstalledLocation.GetFolderAsync("data");
            var simulatorSettingsFile = await installedFolder.GetFileAsync("license.xml");
            await CurrentAppSimulator.ReloadSimulatorAsync(simulatorSettingsFile);

            // TODO: Module 12: Exercise 2: Task 1.2: Store the license information
            _listingInformation = await CurrentAppSimulator.LoadListingInformationAsync();

            _loaded = true;
        }

        private void LicenseInformation_LicenseChanged()
        {
            if (!CurrentAppSimulator.LicenseInformation.IsActive)
                return;

            IsTrialLicense = false;// CurrentAppSimulator.LicenseInformation.IsTrial;

            IsMediaFeatureEnabled = true;// CurrentAppSimulator.LicenseInformation.ProductLicenses["MediaFeature"].IsActive;

            var handler = LicenseDataUpdated;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }


        public async Task PurchaseAppAsync()
        {
            await LoadLicenseData();

            if (IsTrialLicense)
            {
                await CurrentAppSimulator.RequestAppPurchaseAsync(false);
            }
        }

        public async Task PurchaseMediaFeatureAsync()
        {
            var trialMessage = ResourceHelper.ResourceLoader.GetString("TrialMessage");
            var alreadyOwnMessage = ResourceHelper.ResourceLoader.GetString("YouAlreadyOwn");
            var boughtMessage = ResourceHelper.ResourceLoader.GetString("BoughtMessage");
            var unableToBuyMessage = ResourceHelper.ResourceLoader.GetString("UnableToBuy");
            var message = string.Empty;

            if (IsTrialLicense)
            {
                message = trialMessage;
            }
            else
            {
                var product = _listingInformation.ProductListings["MediaFeature"];
                if (IsMediaFeatureEnabled)
                {
                    message = string.Format("{0} '{1}'", alreadyOwnMessage, product.Name);
                }

                else
                {
                    try
                    {
                        await CurrentAppSimulator.RequestProductPurchaseAsync(product.ProductId, false);
                        message = string.Format("{0} '{1}'", boughtMessage, product.Name);
                    }
                    catch
                    {
                        message = string.Format("{0} '{1}'", unableToBuyMessage, product.Name);
                    }
                }
            }
            var msg = new Windows.UI.Popups.MessageDialog(message, "In-App Purchase");
            await msg.ShowAsync();
        }
    }
}
