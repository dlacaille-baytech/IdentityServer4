﻿using IdentityServer4.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Models
{
    /// <summary>
    /// Models a web API resource.
    /// </summary>
    public class ApiResource
    {
        public ApiResource()
        {
        }

        public ApiResource(string scopeName)
            : this(scopeName, scopeName, null)
        {
        }

        public ApiResource(string scopeName, string displayName)
            : this(scopeName, displayName, null)
        {
        }

        public ApiResource(string scopeName, IEnumerable<string> userClaimTypes)
            : this(scopeName, scopeName, userClaimTypes)
        {
        }

        public ApiResource(string scopeName, string displayName, IEnumerable<string> userClaimTypes)
        {
            if (scopeName.IsMissing()) throw new ArgumentNullException(nameof(scopeName));

            Name = scopeName;
            Scopes.Add(new Scope(scopeName, displayName));

            if (!userClaimTypes.IsNullOrEmpty())
            {
                foreach (var type in userClaimTypes)
                {
                    UserClaims.Add(new UserClaim(type));
                }
            }
        }

        /// <summary>
        /// Specifies if API is enabled (defaults to true)
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the api secrets.
        /// </summary>
        public ICollection<Secret> ApiSecrets { get; set; } = new HashSet<Secret>();

        /// <summary>
        /// Gets or sets the scopes.
        /// </summary>
        public ICollection<Scope> Scopes { get; set; } = new HashSet<Scope>();

        /// <summary>
        /// List of user claims that should be included in the access token.
        /// </summary>
        public ICollection<UserClaim> UserClaims { get; set; } = new HashSet<UserClaim>();

        internal ApiResource CloneWithScopes(IEnumerable<Scope> scopes)
        {
            return new ApiResource()
            {
                Enabled = Enabled,
                Name = Name,
                ApiSecrets = ApiSecrets,
                Scopes = new HashSet<Scope>(scopes.ToArray()),
                UserClaims = UserClaims,
            };
        }
    }
}
