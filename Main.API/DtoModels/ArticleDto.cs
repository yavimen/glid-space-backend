﻿using System.ComponentModel.DataAnnotations;

namespace Main.API.DtoModels
{
    public class ArticleDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
    }
}
