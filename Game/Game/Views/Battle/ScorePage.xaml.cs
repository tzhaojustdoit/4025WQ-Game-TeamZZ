﻿using Game.Models;
using Game.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScorePage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        /// <summary>
        /// Constructor
        /// </summary>
        public ScorePage()
        {
            InitializeComponent();
            DrawOutput();
        }

        /// <summary>
        /// Draw data for
        /// Character
        /// Monster
        /// Item
        /// </summary>
        public void DrawOutput()
        {

            // Draw the Characters
            foreach (var data in EngineViewModel.Engine.BattleScore.CharacterModelDeathList)
            {
                CharacterListFrame.Children.Add(CreateCharacterDisplayBox(data));
            }

            // Draw the Monsters
            foreach (var data in EngineViewModel.Engine.BattleScore.MonsterModelDeathList)
            {
                MonsterListFrame.Children.Add(CreateMonsterDisplayBox(data));
            }

            // Draw the Items
            foreach (var data in EngineViewModel.Engine.BattleScore.ItemModelDropList)
            {
                ItemListFrame.Children.Add(CreateItemDisplayBox(data));
            }

            // Update Values in the UI
            TotalKilled.Text = EngineViewModel.Engine.BattleScore.MonsterModelDeathList.Count().ToString();
            TotalCollected.Text = EngineViewModel.Engine.BattleScore.ItemModelDropList.Count().ToString();
            TotalScore.Text = EngineViewModel.Engine.BattleScore.ExperienceGainedTotal.ToString();
        }

        /// <summary>
        /// Return a stack layout for the Characters
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreateCharacterDisplayBox(EntityInfoModel data)
        {
            if (data == null)
            {
                data = new EntityInfoModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["ImageBattleMediumStyle"],
                Source = data.ImageURI
            };

            // Add the Level
            var PlayerLevelLabel = new Label
            {
                Text = "Level : " + data.Level,
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["ScoreCharacterInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                    PlayerLevelLabel,
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Return a stack layout for the Monsters
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreateMonsterDisplayBox(EntityInfoModel data)
        {
            if (data == null)
            {
                data = new EntityInfoModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["ImageBattleSmallStyle"],
                Source = data.ImageURI
            };

            // Add the Level
            var PlayerLevelLabel = new Label
            {
                Text = "Level : " + data.Level,
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["ScoreMonsterInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                    PlayerLevelLabel,
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Return a stack layout with the Player information inside
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreateItemDisplayBox(ItemModel data)
        {
            if (data == null)
            {
                data = new ItemModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["ImageBattleSmallStyle"],
                Source = data.ImageURI
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["ScoreItemInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Attack Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}