/*
 * Copyright (c) Facebook, Inc. and its affiliates.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */

using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;
using Jering.Javascript.NodeJS;
using Microsoft.Extensions.DependencyInjection;
using React.NodeServices;
using React.TinyIoC;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(React.Sample.Mvc4.ReactConfig), "Configure")]

namespace React.Sample.Mvc4
{
	public static class ReactConfig
	{
		public static void Configure()
		{
			var services = new ServiceCollection();
			services.AddNodeJS();
			services.Configure<NodeJSProcessOptions>(options => options.NodeAndV8Options = "--inspect");
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			ReactSiteConfiguration.Configuration
				.SetReuseJavaScriptEngines(true)
				.SetAllowJavaScriptPrecompilation(true)
				//.AddScriptWithoutTransform("~/Content/lib/reactstrap.min.js")
				.SetNodeJsEngine(() => NodeJsEngine.CreateEngine(serviceProvider.GetRequiredService<INodeJSService>()))
				.SetBabelVersion(BabelVersions.Babel7)
				.AddScript("~/Content/Sample.tsx");

			JsEngineSwitcher.Current.DefaultEngineName = V8JsEngine.EngineName;
			JsEngineSwitcher.Current.EngineFactories.AddV8();
		}
	}
}
