﻿using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;


using Xamarin.Forms.Xaml;

namespace Game.Views
{
    
    [DesignTimeVisible(false)]
    public partial class CharacterCreatePage : ContentPage
    {
        GenericViewModel<CharacterModel> ViewModel { get; set; }
        public CharacterCreatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();
            data.Data = new CharacterModel();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Create";

            CharacterClassPicker.SelectedItem = data.Data.CharacterClass.ToString();
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.Data.Name == "")
            {
                await DisplayAlert("Alert", "You need to enter a name", "OK");
                return;
            }

            // If the image in the data box is empty, use the default one..
            if(ViewModel.Data.CharacterClass == CharacterClassEnum.Fighter || ViewModel.Data.CharacterClass == CharacterClassEnum.Cleric)
            {
                MessagingCenter.Send(this, "Create", ViewModel.Data);
                await Navigation.PopModalAsync();
                return;
            }

            await DisplayAlert("Alert", "You need to select a Class", "OK");


        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}