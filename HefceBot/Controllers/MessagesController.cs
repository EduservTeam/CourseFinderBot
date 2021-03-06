﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HefceBot.Models;
using HefceBot.Services;
using Microsoft.Bot.Connector;
//using Microsoft.Bot.Connector.Utilities;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace HefceBot.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private static IUnistatsService _unistatsService;

        public MessagesController(IUnistatsService unistatsService)
        {
            _unistatsService = unistatsService;
        }

        internal static IDialog<HefceUserSearchRequest> MakeRootDialog()
        {

            return Chain.From(() => FormDialog.FromForm(HefceUserSearchRequest.BuildForm))
                .Do(async (context, order) =>
                {
                    try
                    {
                        var completed = await order;

                        var attendanceType = completed.AttendanceType;
                        var institutionSearchTerm = completed.InstitutionSearchText;
                        var courseSearchText = completed.CourseSearchText;

                        IEnumerable<CourseWithInstitution> courses;
                        if (attendanceType == AttendanceOptions.Both)
                        {
                            courses = _unistatsService.GetTopCourses(institutionSearchTerm, courseSearchText);
                        }
                        else
                        {
                            courses = _unistatsService.GetTopCourses(institutionSearchTerm, courseSearchText, attendanceType);
                        }

                        string x = $"Try these course suggestions: \n\n{string.Join("\n\n", courses.Select(c => $"{c.Title} at {c.Institution.Name}: {c.WebUrl ?? ""}"))}";

                        await context.PostAsync(x);
                    }
                    catch (FormCanceledException<HefceUserSearchRequest> e)
                    {
                    }
                });
        }


        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody] Message message)
        {
            if (message.Type == "Message")
            {
                // calculate something for us to return
                //int length = (message.Text ?? string.Empty).Length;
                // return our reply to the user
                //return message.CreateReplyMessage($"You sent {length} characters");
                var responseFromLewis = await LuisResponse.ParseUserInput(message.Text);
                if (responseFromLewis.intents.Any())
                {
                    switch (responseFromLewis.intents[0].intent)
                    {
                        case "FindUni":
                            message.Text = responseFromLewis.entities[0].entity;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    return message.CreateReplyMessage("Sorry, I don't understand..."); ;
                }

                return await Conversation.SendAsync(message, MakeRootDialog);


            }
            else
            {
                return HandleSystemMessage(message);
            }
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
                var robin = 1;
            }

            return null;
        }
    }
}