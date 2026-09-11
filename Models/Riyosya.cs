using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppApi.Models
{
    [Table("Riyosya")]
    [PrimaryKey(nameof(Riyosha_Id),(nameof(agri_code)))]
    public class Riyosya
    {
        [Column(TypeName = "varchar(40)")]
        public string Riyosha_Id { get; set; } = string.Empty;

        [Column(TypeName = "int")]
        public int? agri_code { get; set; }

        [MaxLength(100)]
        public string? name { get; set; }

        [Column(TypeName = "varchar(40)")]
        public string? password { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? lebel { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? daikou { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? builtin { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? delflg { get; set; }

        [Column(TypeName = "varchar(8)")]
        public string? zip_code { get; set; }

        [MaxLength(40)]
        public string? address1 { get; set; }

        [MaxLength(40)]
        public string? address2 { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? tel { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? etax_user_id { get; set; }

        [Column(TypeName = "int")]
        public int? branch_code { get; set; }

        [Column(TypeName = "int")]
        public int? uni_k_code { get; set; }

        public DateTime? update_time { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? policy { get; set; }

        public DateTime? pass_updatetime { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? use_client { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? enable_account { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? use_bwh_client { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? use_boki { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? use_shinkoku { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? use_bunseki { get; set; }

        [Column(TypeName = "int")]
        public int? reporter_code { get; set; }

        [Column(TypeName = "int")]
        public int? zei_type { get; set; }

        [MaxLength(80)]
        public string? zei_address1 { get; set; }

        [MaxLength(80)]
        public string? zei_address2 { get; set; }

        [MaxLength(20)]
        public string? zei_tel { get; set; }

        [MaxLength(8)]
        public string? zei_group { get; set; }

        [MaxLength(8)]
        public string? zei_sub_group { get; set; }

        [MaxLength(10)]
        public string? zei_no { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? read_only { get; set; }

        [Column(TypeName = "varchar(40)")]
        public string? parent_id { get; set; }

        [MaxLength(8)]
        public string? zei_number { get; set; }

        [MaxLength(20)]
        public string? bk_serial_number { get; set; }

        [MaxLength(1000)]
        public string? contract_code { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? use_etax { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? use_kachiku { get; set; }

        public DateTime? regist_time { get; set; }

        public DateTime? end_time { get; set; }

        public string? note { get; set; }

        public short? login_year { get; set; }

        [Column(TypeName = "tinyint")]
        public byte? account_type { get; set; }

        public DateTime? createday { get; set; }

        [MaxLength(20)]
        public string? hojin_bango { get; set; }

        [MaxLength(150)]
        public string? connect_key { get; set; }
    }
}