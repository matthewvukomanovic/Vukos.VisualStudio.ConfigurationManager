using System;

namespace Vukos.Common
{
    /// <summary>
    /// Used to suppress an action from occurring in code.
    /// Generally used to stop an action from occurring twice.
    /// Not Thread Safe.
    /// </summary>
    public class Suppression
    {
        #region Constants

        /// <summary>
        /// The default suppression count
        /// </summary>
        const int DEFAULT_SuppressionCount = 0;

        #endregion

        #region Constructor

        public Suppression(  )
        {

        }

        #endregion

        #region Variables and Properties

        /// <summary>
        /// Used to store the current number of suppressions on the object.
        /// </summary>
        private int _suppressionCount = DEFAULT_SuppressionCount;

        /// <summary>
        /// Gets a value indicating whether this instance is suppressed.
        /// Not Thread Safe.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is suppressed; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuppressed
        {
            get
            {
                return _suppressionCount != DEFAULT_SuppressionCount;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Increments the suppression count, this can be called multiple times.
        /// A call to <see cref="Unsuppress"/> should be made for every call to this method.
        /// Not Thread Safe.
        /// </summary>
        public void Suppress()
        {
            _suppressionCount++;
        }

        /// <summary>
        /// Decrements the suppression count, this can be called multiple times.
        /// A call to <see cref="Suppress"/> should have been made first for every call to this method.
        /// Not Thread Safe.
        /// </summary>
        public void Unsuppress()
        {
            _suppressionCount--;
            System.Diagnostics.Debug.Assert(_suppressionCount >= DEFAULT_SuppressionCount, "There has been more Un-suppress calls than Suppress calls, this means that there is an issue with your code.");
        }

        /// <summary>
        /// Runs the specified action, only allowing it to be run once.
        /// </summary>
        /// <param name="singleUsageAction">The action which should only be run once at a time.</param>
        public void Run(Action singleUsageAction)
        {
            // Check if the object is already suppressed.
            if (this.IsSuppressed)
            {
                // if it is already happening then don't run it again
                return;
            }

            try
            {
                this.Suppress();

                // Run the action which should only run once.
                singleUsageAction();
            }
            finally
            {
                this.Unsuppress();
            }
        }

        #endregion
    }
}
