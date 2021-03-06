﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using PlanMy.Droid;

[assembly: ResolutionGroupName("Refractored")]
[assembly: ExportEffect(typeof(ListViewBottomEffect), "ListViewBottomEffect")]
namespace PlanMy.Droid
{
    public class ListViewBottomEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var listView = Control as AbsListView;

                if (listView == null)
                    return;

                listView.SetDrawSelectorOnTop(true);
                listView.StackFromBottom = true;
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnDetached()
        {

        }
    }
}