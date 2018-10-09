# MyGlucose (.Net Core doctor application)
The doctor's view/backend website for the MyGlucose application

To use the web server during testing, you will need to configure IIS Express to allow external access (from mobile devices or other computers):
1. Open a port in Windows Firewall:
   - Control Panel > Windows Defender Firewall > Advanced Settings… > Inbound Rules > New Rule… > TCP > Provide your port number > Allow all > [Finish]
2. Set up IIS Express to listen on that port when it starts:
   - [Project Folder] > .vs > config > applicationhost.config
   - Find the entry for your app, and there will be lines for http and https:
```
<site...>
	<bindings>
		<binding protocol="http" bindingInformation="*:12345:localhost" />
		<binding protocol="https" bindingInformation="*:54321:localhost" />
```

   - Copy and paste the lines, changing "localhost" to * (using the same port numbers):

```
		<binding protocol="http" bindingInformation="*:12345:*" />
		<binding protocol="https" bindingInformation="*:54321:*" />
	</bindings>
</site>
```
   - Remember to leave the first 2 lines so that testing will still work in your browser
   - **!!!NOTE!!! You _MUST_ start Visual Studio with Administrator rights so that IIS will bind the wildcard addresses.** Otherwise, it will give an error message _"The hostname could not be parsed"_
