using Invert.Core.GraphDesigner;

namespace Invert.uFrame.MVVM {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    
    
    public class ViewPage : ViewPageBase {
        
        public override void GetContent(Invert.Core.GraphDesigner.IDocumentationBuilder _)
        {
            base.GetContent(_);

            _.Title2("What Is It?");
            _.Paragraph("Views are perhaps the easiest layer to understand in the context of Unity, due to the fact that they exist specifically to interact with the UnityEngine namespace and environment.  Views are the \"presentation\" layer, where the data of ViewModels are represented through the implementation of bindings.  The idea is that for the most part, ViewModel data exists somewhere already, and Views merely \"bind\" to that data in order to represent changes in a way that Unity and players can understand.");
            _.Title2("Where does it exist in Unity?");
            _.Paragraph("Views generated by uFrame inherit from Monobehaviour, and therefore are much like normal Unity components.  Building on top of Monobehaviour, uFrame ties into Unity methods like Update, Start, OnEnable, and OnDestroy in order to implement necessary MVCVM functionality within the Unity environment.  Every View that uFrame generates is meant to exist as a component on a particular GameObject.  For instance, a PlayerView should probably exist on some kind of Player GameObject, and a PlayerHUDView should probably exist on some kind of GUI GameObject to bind to and express a player's stats and other properties.");
            
            _.Title2("What Else?");
            _.Title3("The Presentation Layer");
            
            _.Paragraph("Views are the so-called \"presentation\" layer, where a programmer will implement the logic of how the abstracted ViewModel data is represented in a particular environment.  So if you have a PlayerViewModel, you may decide to represent that in any number of ways, including:");
            _.Paragraph(" - a PlayerView that represents the player as an animated character moving in space");
            _.Paragraph(" - a PlayerHUDView that represents the player's health, stamina, energy and other stats in your GUI");
            _.Paragraph(" - and maybe a PlayerMapView that represents your player's position relative to some kind of GUI Map object");

            _.Paragraph("All of these would typically want to bind to the same player ViewModel \"instance\", such that they are said to share the same ViewModel, representing the data in different ways.  The most important distinction is that each of these views should concern themselves with ONLY their own representation, meaning that the PlayerView in the above example should not be updating GUI elements, but rather leave that to the PlayerHUDView or possibly PlayerMapView.  Views should be as independent as possible, handling just themselves and their own interactions.  Since any number of Views can bind to the same ViewModel instance, it is up to you to determine how many Views are needed and what their individual responsibilities will be in representing that data inside Unity.");


            ImportantMethods(_);
        }

        private static void ImportantMethods(IDocumentationBuilder _)
        {
            _.Title2("Execution Order");
            _.Paragraph("There are actually several different entry points on generated Views.  The usual order is:");
            _.Title3("For Views instantiated at runtime");
            _.Paragraph("Awake > OnEnable > PreBind > Bind > AfterBind > InitializeViewModel > Start > Update loop begins");
            _.Title3("For Views existing \"SceneFirst\" before runtime");
            _.Paragraph(
                "Awake > OnEnable > CreateModel > InitializeViewModel > Start (before base call) > PreBind > Bind > AfterBind > Start (after base call)");

            _.Title3("When Destroying an object");
            _.Paragraph(
                "OnDisable > OnDestroy (before base.OnDestroy() call) > UnBind > OnDestroy (after base.OnDestroy() call)");
            _.Break();
            _.Break();
            _.Title2("Help, my bindings have stopped working!");
            _.Paragraph(
                "There are a few methods that ALWAYS need their base.Method() calls intact, otherwise uFrame can easily  produce unexpected results.");
            _.Paragraph("These methods include a majority of the overridden standard Unity methods:");
            _.Paragraph(" - Awake(), Start(), OnEnable(), OnDisable(), OnDestroy(), Update(), LateUpdate()");
            _.Paragraph(" - PreBind(), Bind(), AfterBind(), UnBind(), InitializeViewModel()");
            _.Break();
            _.Break();
            _.Title2("Important Methods");
            _.Paragraph(
                "When looking for more clarity on how uFrame builds upon Monobehaviour, it can be fairly useful to look through ViewBase.cs, as this is what all uFrame Views inherit from.");

            _.Title3("PreBind()");
            _.Paragraph("This happens before the View begins creating bindings to its given ViewModel.");
            _.Break();

            _.Title3("Bind()");
            _.Paragraph(
                "This is where the View actually creates property bindings, collection bindings, and command bindings to the given ViewModel.  The base.Bind() call will automatically create the bindings specified in the uFrame diagram for this specific View type.  If you have any further manual bindings you need to do, this can be a good place to implement them.");
            _.Break();

            _.Title3("AfterBind()");
            _.Paragraph("This is called immediately after the View creates bindings to its ViewModel.");
            _.Break();

            _.Title3("CreateModel()");
            _.Paragraph(
                "This is when SceneFirst Views request a proper ViewModel from the scene's Dependency Container.  For the most part, this should be left alone.");
            _.Break();

            _.Title3("InitializeViewModel()");
            _.Paragraph(
                "On a View, when the Initialize ViewModel option is checked in the inspector, this is where the base.InitializeViewModel() call will set the ViewModel's properties to the values of the View's matching properties (which are underscored in code on the View).  This will usually never need to be overridden.");
            _.Break();

            _.Title3("Awake(), Start(), OnEnable(), OnDisable(), OnDestroy(), Update(), LateUpdate()");
            _.Paragraph(
                "These are all the same as their Unity counterparts, and must retain their base calls if you override them, in order for uFrame to function properly.");
            _.Break();
        }
    }
}
