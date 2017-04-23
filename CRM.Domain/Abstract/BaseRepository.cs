using Microsoft.AspNetCore.Http;
using System;

namespace CRM.Domain.Abstract
{
    public class BaseRepository
    {
        private string _userid;

        public string UserContext { get; set; }

        public void SetCreatedSignature(IEntity item)
        {
            _userid = UserContext;
            if (_userid!=null) item.CreatedByID = _userid;
            item.CreatedTime = DateTime.Now;
            if (_userid != null) item.ModifiedByID = _userid;
            item.ModifiedTime = DateTime.Now;
        }

        public void SetModifiedSignature(IEntity item)
        {
            _userid = UserContext;
            if (_userid != null) item.ModifiedByID = _userid;
            item.ModifiedTime = DateTime.Now;
        }
    }
}

