using System;
using System.Collections.Generic;

namespace Manualfac
{
    class Disposer : Disposable
    {
        #region Please implements the following methods

        /*
         * The disposer is used for disposing all disposable items added when it is disposed.
         */

        List<IDisposable> items = new List<IDisposable>();

        public void AddItemsToDispose(object item)
        {
            var disposable = item as IDisposable;
            if (disposable != null)
            {
                items.Add(disposable);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                for (var i = items.Count - 1; i >= 0; i--)
                {
                    items[i].Dispose();
                }
                items = null;
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}