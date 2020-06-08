﻿using Reception.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reception.Server.File.Model
{
    [Table("FileData", Schema = "Files")]
    public class FileData : IUnique
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public byte[] Value { get; set; }

        [Required]
        public VersionInfo VersionInfo { get; set; }
    }
}