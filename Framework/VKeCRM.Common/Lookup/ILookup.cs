namespace VKeCRM.Common.Lookups
{
	public interface ILookup
	{
		#region Properties
		/// <summary>
		/// Gets the Code for the Lookup object.
		/// </summary>
		short Code
		{
			get;
		}

		/// <summary>
		/// Gets the Description of the Lookup object.
		/// </summary>
		string Description
		{
			get;
		}

		/// <summary>
		/// Gets the Display Name of the Lookup object.
		/// This field can be bind to the presentation layer controls.
		/// </summary>
		string DisplayName
		{
			get;
		}

		/// <summary>
		/// Shows whether this Lookup object is active. 
		/// </summary>
		bool IsActive
		{
			get;
		}

		/// <summary>
		/// Gets the Sort Rank of the Lookup object
		/// </summary>
		int SortRank
		{
			get;
		}
		#endregion

	}
}
