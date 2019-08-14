# Multi Keyboards
Adds support for low end bar code scanners, which work as a keyboard only.

This is a full solution for getting code scanned from multiple low end bar code scanners, including:
* Multiple scanners support
* Characters combine until stop sign (e.g., Enter)
* User-defined key-to-text mapping

# Change log
* 1.2: Numeric panel keys support
* 1.1: Pass more information to Appender

# Note
Package is built on dotNet framework 4.0 and support x86 (32bit) platform target only. You can use this code on x64 but still need to keep the platform target for build x86 instead of Any CPU or x64.

# Nuget
Package is published [here](https://www.nuget.org/packages/SecretNest.MultiKeyboards).

# Demo
There is a demo built as WinForm in demo folder.

In this demo, you could link multiple keyboard based bar code scanners to scan codes.

You need to prepare some special codes for naming device.
* 00xxx: Set the device name to xxx. xxx is a 3-digit number and not 000.
* 00000: Unset the device.
* 99xxx: Validate the name of the device is xxx or not. xxx is a 3-digit number and not 000.
* 99000: Validate the device is not named.
* =====: Set this device to direct output mode (for keyboard).
* Other number based code: Output.

New scanned code or command will be output to the top of the form.

You can also check the file AsciiCodeAppender.cs for supporting ASCII code between 32 and 126 from scanner.

# Thanks
Many thanks to Emma Burrows and Steve Messer. They explained the API of Windows in [this article](https://www.codeproject.com/Articles/17123/Using-Raw-Input-from-C-to-handle-multiple-keyboard).

# License
* Test and Demo project are released under MIT license.
* Other projects are released under LGPL-3.0 license.
