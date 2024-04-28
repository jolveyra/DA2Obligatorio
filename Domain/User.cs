﻿namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is User u && u.Id == Id;
        }
    }
}
