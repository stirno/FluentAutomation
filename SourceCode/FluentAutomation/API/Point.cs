// <copyright file="Point.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

namespace FluentAutomation.API
{
    /// <summary>
    /// Stores X, Y coordinates used for click/hover/drag/drop
    /// </summary>
    public class Point
    {
        private int _x = 0;
        private int _y = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Gets or sets the X-axis value.
        /// </summary>
        /// <value>
        /// X-axis value.
        /// </value>
        public int X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y-axis value.
        /// </summary>
        /// <value>
        /// Y-axis value.
        /// </value>
        public int Y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
            }
        }
    }
}
