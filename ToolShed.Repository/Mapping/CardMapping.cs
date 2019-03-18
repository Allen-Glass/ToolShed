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

        public static Models.Repository.UserCard CreateUserCardDTO(Card card)
        {
            return new Models.Repository.UserCard
            {
                UserId = card.UserId,
                CardId = card.CardId
            };
        }

        public static Card ConvertDtoCardToCard(Models.Repository.Card card)
        {
            return new Card
            {
                UserId = card.UserId,
                CardId = card.CardId,
                CardHolderName = card.CardHolderName,
                CardNumber = card.CardNumber
            };
        }

        public static IEnumerable<Card> ConvertDtoCardsToCards(IEnumerable<Models.Repository.Card> cards)
        {
            var cardList = new List<Card>();
            foreach (var card in cards)
            {
                cardList.Add(ConvertDtoCardToCard(card));
            }

            return cardList;
        }
    }
}
