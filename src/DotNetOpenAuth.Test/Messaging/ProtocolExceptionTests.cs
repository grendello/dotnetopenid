﻿//-----------------------------------------------------------------------
// <copyright file="ProtocolExceptionTests.cs" company="Andrew Arnott">
//     Copyright (c) Andrew Arnott. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DotNetOpenAuth.Test.Messaging {
	using System;
	using DotNetOpenAuth.Messaging;
	using NUnit.Framework;

	[TestFixture]
	public class ProtocolExceptionTests : TestBase {
		[TestCase]
		public void CtorDefault() {
			ProtocolException ex = new ProtocolException();
		}

		[TestCase]
		public void CtorWithTextMessage() {
			ProtocolException ex = new ProtocolException("message");
			Assert.AreEqual("message", ex.Message);
		}

		[TestCase]
		public void CtorWithTextMessageAndInnerException() {
			Exception innerException = new Exception();
			ProtocolException ex = new ProtocolException("message", innerException);
			Assert.AreEqual("message", ex.Message);
			Assert.AreSame(innerException, ex.InnerException);
		}

		[TestCase]
		public void CtorWithProtocolMessage() {
			IProtocolMessage message = new Mocks.TestDirectedMessage();
			ProtocolException ex = new ProtocolException("message", message);
			Assert.AreSame(message, ex.FaultedMessage);
		}

		[TestCase, ExpectedException(typeof(ArgumentNullException))]
		public void CtorWithNullProtocolMessage() {
			new ProtocolException("message", (IProtocolMessage)null);
		}
	}
}
