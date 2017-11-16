# qPing

A very simple, multiple, consecutive IPV4-address ping utility (Windows).

Build using MS Visual Studio 2017 or later.

Run qPing.exe, under Windows command prompt.

Usage:
        qping ipaddress [range-count [ping-count]]

        ipaddress      - valid IPV4 address
        address-count  - (optional) number of addresses to ping (default: 4)
        ping-count     - (optional)number of pings to send to each address (default: 4)

        For example, the following will ping 89.150.31.60, then 89.150.31.61, then 89.150.31.62 - twice each

        qping 89.150.31.60 3 2
