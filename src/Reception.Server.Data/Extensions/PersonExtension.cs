﻿using Reception.Model.Dto;
using Reception.Server.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Reception.Server.Data.Extensions
{
    public static class PersonExtension
    {
        public static PersonDto ToDto(this Person person)
        {
            return
                person == null ?
                null :
                new PersonDto
                {
                    Comment = person.Comment,
                    FirstName = person.FirstName,
                    MiddleName = person.MiddleName,
                    Post = person.Post.ToDto(),
                    SecondName = person.SecondName,
                    Id = person.Id
                };
        }

        public static IEnumerable<PersonDto> ToDtos(this IEnumerable<Person> valueList)
        {
            return valueList.Select(value => value.ToDto()).ToList();
        }
    }
}