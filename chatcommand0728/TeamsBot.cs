﻿using Microsoft.Agents.Builder;
using Microsoft.Agents.Builder.App;
using Microsoft.Agents.Builder.State;
using Microsoft.Agents.Core.Models;
using Microsoft.TeamsFx.Conversation;

namespace chatcommand0728
{
    /// <summary>
    /// Bot handler.
    /// You can add your customization code here to extend your bot logic if needed.
    /// </summary>
    public class TeamsBot : AgentApplication
    {
        private readonly ConversationBot _conversation;
        
        public TeamsBot(AgentApplicationOptions options, ConversationBot conversation) : base(options)
        {
            _conversation = conversation;
            
            OnConversationUpdate(ConversationUpdateEvents.MembersAdded, OnMembersAddedAsync);
            
            // Listen for ANY message to be received. MUST BE AFTER ANY OTHER MESSAGE HANDLERS
            OnActivity(ActivityTypes.Message, OnMessageReceivedAsync);
        }

        protected async Task OnMembersAddedAsync(ITurnContext turnContext, ITurnState turnState, CancellationToken cancellationToken)
        {
            var welcomeText = "Welcome to the Command Bot! I can help you with a few simple commands. Type \"helloworld\" or \"help\" to get started.";
            foreach (var member in turnContext.Activity.MembersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText), cancellationToken);
                }
            }
        }
        
        protected async Task OnMessageReceivedAsync(ITurnContext turnContext, ITurnState turnState, CancellationToken cancellationToken)
        {
            // Use the conversation command handler to process the message
            // This will be handled by the command handlers registered in Program.cs
            await turnContext.SendActivityAsync(MessageFactory.Text($"Processed your command: {turnContext.Activity.Text}"), cancellationToken);
        }
    }
}
