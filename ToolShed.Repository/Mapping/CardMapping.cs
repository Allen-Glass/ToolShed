using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Models.API;

namespace ToolShed.Repository.Mapping
{
    public static class CardMapping
    {
        public static Models.Repository.Card CreateDtoCard(Card card)
        {
            return new Models.Repository.Card
            {
                CardHolderName = card.CardHolderName,
                CardNumber = card.CardNumber,
                UserId = card.UserId
            };
        }

        public static Models.Repository.UserCard CreateUserCardDTO(Guid userId, Guid cardId)
        {
            return new Models.Repository.UserCard
            {
                UserId = userId,
                CardId = cardId
            };
        }
    }
}
