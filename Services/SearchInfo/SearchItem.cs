﻿using CatalogR.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Services.SearchInfo
{
    public class SearchItem : Searchable<Item>
    {
        public override List<Expression<Func<Item, bool>>> GetPredicates(string query)
        {
            var search = GetSearchString(query);
            return new()
            {
                i => EF.Functions.Contains(i.Name, search),
                i => i.Collection!.CustomString1_State && EF.Functions.Contains(i.CustomString1!, search),
                i => i.Collection!.CustomString2_State && EF.Functions.Contains(i.CustomString2!, search),
                i => i.Collection!.CustomString3_State && EF.Functions.Contains(i.CustomString3!, search),
            };
        }

        public override Expression<Func<Item, Item>> Selector
        {
            get
            {
                Expression<Func<Item, Item>> selector = i =>
                new()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Collection = i.Collection,
                    CustomString1 = i.CustomString1,
                    CustomString2 = i.CustomString2,
                    CustomString3 = i.CustomString3,
                    TimeStamp = i.TimeStamp
                };

                return selector;
            }
        }
    }
}
