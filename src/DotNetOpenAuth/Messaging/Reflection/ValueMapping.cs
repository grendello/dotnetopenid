﻿//-----------------------------------------------------------------------
// <copyright file="ValueMapping.cs" company="Andrew Arnott">
//     Copyright (c) Andrew Arnott. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DotNetOpenAuth.Messaging.Reflection {
	using System;
	using System.Diagnostics.Contracts;

	/// <summary>
	/// A pair of conversion functions to map some type to a string and back again.
	/// </summary>
	[ContractVerification(true)]
	internal struct ValueMapping {
		/// <summary>
		/// The mapping function that converts some custom type to a string.
		/// </summary>
		internal readonly Func<object, string> ValueToString;

		/// <summary>
		/// The mapping function that converts a string to some custom type.
		/// </summary>
		internal readonly Func<string, object> StringToValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="ValueMapping"/> struct.
		/// </summary>
		/// <param name="toString">The mapping function that converts some custom type to a string.</param>
		/// <param name="toValue">The mapping function that converts a string to some custom type.</param>
		internal ValueMapping(Func<object, string> toString, Func<string, object> toValue) {
			Contract.Requires<ArgumentNullException>(toString != null);
			Contract.Requires<ArgumentNullException>(toValue != null);

			this.ValueToString = toString;
			this.StringToValue = toValue;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValueMapping"/> struct.
		/// </summary>
		/// <param name="encoder">The encoder.</param>
		internal ValueMapping(IMessagePartEncoder encoder) {
			Contract.Requires<ArgumentNullException>(encoder != null);
			var nullEncoder = encoder as IMessagePartNullEncoder;
			string nullString = nullEncoder != null ? nullEncoder.EncodedNullValue : null;

			this.ValueToString = obj => (obj != null) ? encoder.Encode(obj) : nullString;
			this.StringToValue = str => (str != null) ? encoder.Decode(str) : null;
		}
	}
}
