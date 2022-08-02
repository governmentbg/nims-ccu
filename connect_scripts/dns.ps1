# Set primary DNS server to google
$networkInterfaceIndex = Get-NetAdapter -InterfaceDescription "Cisco *" | Select -ExpandProperty "ifindex"
Set-DNSClientServerAddress -InterfaceIndex $networkInterfaceIndex -ServerAddresses ("8.8.8.8")
& ipconfig /flushdns