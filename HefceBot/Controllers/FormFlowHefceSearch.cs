using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;

#pragma warning disable 649

namespace HefceBot.Controllers
{
    public enum AttendanceOptions
    {
        FullTime = 1, PartTime, Both
    };

    [Serializable]

    public class HefceUserSearchRequest
    {
        [Prompt("Enter a search term for the Institution name you are looking for")]
        public string InstitutionSearchText;

        [Prompt("Enter a search term for the course name you are looking for")]
        public string CourseSearchText;

        [Prompt("What type of attendance option are you looking for? {||}")]
        public AttendanceOptions? AttendanceType;

        public static IForm<HefceUserSearchRequest> BuildForm()
        {
            //OnCompletionAsyncDelegate<HefceUserSearchRequest> processOrder = async (context, state) =>

            //{

            //    await context.PostAsync(state.);

            //};



            return new FormBuilder<HefceUserSearchRequest>()

                        .Message("Welcome to the Hefce search assistant.")
                        //.AddRemainingFields()
                        //.Message("The results are...")

                        .Build();

        }

    };

}