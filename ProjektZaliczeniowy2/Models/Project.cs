﻿namespace ProjektZaliczeniowy2.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public List<TaskItem> Tasks { get; set; } = new();
    }
}
