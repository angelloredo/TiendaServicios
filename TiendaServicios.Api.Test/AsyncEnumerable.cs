﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Test
{
    public class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {

        public AsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }

        public AsyncEnumerable(Expression expression) : base(expression)
        {

        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
        {
            return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());

        }

        IQueryProvider IQueryable.Provider
        {
            get { return new AsyncQueryProvider<T>(this); }
        }
    }
}
