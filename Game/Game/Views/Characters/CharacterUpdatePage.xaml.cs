﻿using Game.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.ViewModels;
using Game.Models;
using Game.Services;

namespace Game.Views
{
    /// <summary>
    /// Character Update Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterUpdatePage : ContentPage
    {
        // View Model for Character
        readonly GenericViewModel<CharacterModel> ViewModel;

        // The Original ImageURI for existed Character
        string OriginalImageURi;

        /// <summary>
        /// Constructor that takes an existing data item
        /// </summary>
        public CharacterUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();


            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;
            
            OriginalImageURi = data.Data.ImageURI;
            
            ImagePic.SelectedItem = data.Data.ImageURI;
            //ImagePic.ItemsSource = CharacterService.CharacterURIs;

            

            PrimaryHandPic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.PrimaryHand));

            HeadPic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.Head));

            NecklacePic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.Necklass));

            OffHandPic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.OffHand));

            RightFingerPic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.RightFinger));

            LeftFingerPic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.LeftFinger));

            FeetPic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.Feet));
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            // If the input Name is Empty, display alert and return 
            if (string.IsNullOrEmpty(ViewModel.Data.Name))
            {
                await DisplayAlert("Alert", "You need to enter a name", "OK");
                return;
            }

            // If the image in the data box is empty, use the original one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = OriginalImageURi;
           }

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Stepper for Level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Level_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            LevelValue.Text = String.Format("{0}", e.NewValue);
            ManageHealth();
        }

        /// <summary>
        /// Change the Level Picker
        /// </summary>
        public void ManageHealth()
        {
            // Roll for new HP
            ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the Level
            var level = this.ViewModel.Data.Level;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;

            ManageHealth();

            return true;
        }


        /// <summary>
        /// Randomize the Character, keep the level the same
        /// </summary>
        /// <returns></returns>
        public bool RandomizeCharacter()
        {

            ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

            UpdatePageBindingContext();

            return true;
        }
      
    }
}