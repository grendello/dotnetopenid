﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;
using System.Diagnostics;
using DotNetOpenId.Test.Hosting;
using System.Text.RegularExpressions;

namespace DotNetOpenId.Test.Hosting.Tests {
	[TestFixture]
	public class AspNetHostTest {
		public static readonly string TestWebDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\src\DotNetOpenId.TestWeb"));
		public const string HostTestPage = "HostTest.aspx";
		AspNetHost host;

		[SetUp]
		public void SetUpHost() {
			host = AspNetHost.CreateHost(TestWebDirectory);
		}

		[Test]
		public void TestHost() {
			StringWriter sw = new StringWriter();
			string query =  "a=b&c=d";
			string body = "aa=bb&cc=dd";
			Stream bodyStream = new MemoryStream(Encoding.ASCII.GetBytes(body));
			host.ProcessRequest(HostTestPage,query, bodyStream, sw);
			string resultHtml = sw.ToString();
			Assert.IsFalse(string.IsNullOrEmpty(resultHtml));
			Debug.WriteLine(resultHtml);
			Assert.IsTrue(Regex.IsMatch(resultHtml, @"Query.*" + Regex.Escape(query)));
			Assert.IsTrue(Regex.IsMatch(resultHtml, @"Body.*" + Regex.Escape(body)));
		}
	}
}
