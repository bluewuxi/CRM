using Microsoft.EntityFrameworkCore;
using System;

namespace CRM.Domain.Abstract
{
    public class BaseRepository
    {
        string _userid;

        public void AttachUserContext(string userid)
        {
            _userid = userid;
        }

        public void SetCreatedSignature(IEntity item)
        {
            if (_userid!=null) item.CreatedByID = _userid;
            item.CreatedTime = DateTime.Now;
            if (_userid != null) item.ModifiedByID = _userid;
            item.ModifiedTime = DateTime.Now;
        }

        public void SetModifiedSignature(IEntity item)
        {
            if (_userid != null) item.ModifiedByID = _userid;
            item.ModifiedTime = DateTime.Now;
        }
    }
}

