
# http://www.activexperts.com/xmstoolkit/sms/gsmerrorcodes/
function ATGet-CmeDescription
{
    param
    (
        [int] $code
    )


    $cmeMap = @{}
    $cmeMap.Add( 0, 'Phone failure' ) 
    $cmeMap.Add( 1, 'No connection to phone' ) 
    $cmeMap.Add( 2, 'Phone adapter link reserved' ) 
    $cmeMap.Add( 3, 'Operation not allowed' ) 
    $cmeMap.Add( 4, 'Operation not supported' ) 
    $cmeMap.Add( 5, 'PH_SIM PIN required' ) 
    $cmeMap.Add( 6, 'PH_FSIM PIN required' ) 
    $cmeMap.Add( 7, 'PH_FSIM PUK required' ) 
    $cmeMap.Add( 10, 'SIM not inserted' ) 
    $cmeMap.Add( 11, 'SIM PIN required' ) 
    $cmeMap.Add( 12, 'SIM PUK required' ) 
    $cmeMap.Add( 13, 'SIM failure' ) 
    $cmeMap.Add( 14, 'SIM busy' ) 
    $cmeMap.Add( 15, 'SIM wrong' ) 
    $cmeMap.Add( 16, 'Incorrect password' ) 
    $cmeMap.Add( 17, 'SIM PIN2 required' ) 
    $cmeMap.Add( 18, 'SIM PUK2 required' ) 
    $cmeMap.Add( 20, 'Memory full' ) 
    $cmeMap.Add( 21, 'Invalid index' ) 
    $cmeMap.Add( 22, 'Not found' ) 
    $cmeMap.Add( 23, 'Memory failure' ) 
    $cmeMap.Add( 24, 'Text string too long' ) 
    $cmeMap.Add( 25, 'Invalid characters in text string' ) 
    $cmeMap.Add( 26, 'Dial string too long' ) 
    $cmeMap.Add( 27, 'Invalid characters in dial string' ) 
    $cmeMap.Add( 30, 'No network service' ) 
    $cmeMap.Add( 31, 'Network timeout' ) 
    $cmeMap.Add( 32, 'Network not allowed, emergency calls only' ) 
    $cmeMap.Add( 40, 'Network personalization PIN required' ) 
    $cmeMap.Add( 41, 'Network personalization PUK required' ) 
    $cmeMap.Add( 42, 'Network subset personalization PIN required' ) 
    $cmeMap.Add( 43, 'Network subset personalization PUK required' ) 
    $cmeMap.Add( 44, 'Service provider personalization PIN required' ) 
    $cmeMap.Add( 45, 'Service provider personalization PUK required' ) 
    $cmeMap.Add( 46, 'Corporate personalization PIN required' ) 
    $cmeMap.Add( 47, 'Corporate personalization PUK required' ) 
    $cmeMap.Add( 48, 'PH-SIM PUK required' ) 
    $cmeMap.Add( 100, 'Unknown error' ) 
    $cmeMap.Add( 103, 'Illegal MS' ) 
    $cmeMap.Add( 106, 'Illegal ME' ) 
    $cmeMap.Add( 107, 'GPRS services not allowed' ) 
    $cmeMap.Add( 111, 'PLMN not allowed' ) 
    $cmeMap.Add( 112, 'Location area not allowed' ) 
    $cmeMap.Add( 113, 'Roaming not allowed in this location area' ) 
    $cmeMap.Add( 126, 'Operation temporary not allowed' ) 
    $cmeMap.Add( 132, 'Service operation not supported' ) 
    $cmeMap.Add( 133, 'Requested service option not subscribed' ) 
    $cmeMap.Add( 134, 'Service option temporary out of order' ) 
    $cmeMap.Add( 148, 'Unspecified GPRS error' ) 
    $cmeMap.Add( 149, 'PDP authentication failure' ) 
    $cmeMap.Add( 150, 'Invalid mobile class' ) 
    $cmeMap.Add( 256, 'Operation temporarily not allowed' ) 
    $cmeMap.Add( 257, 'Call barred' ) 
    $cmeMap.Add( 258, 'Phone is busy' ) 
    $cmeMap.Add( 259, 'User abort' ) 
    $cmeMap.Add( 260, 'Invalid dial string' ) 
    $cmeMap.Add( 261, 'SS not executed' ) 
    $cmeMap.Add( 262, 'SIM Blocked' ) 
    $cmeMap.Add( 263, 'Invalid block' ) 
    $cmeMap.Add( 772, 'SIM powered down' )

    $cmeMap[$code]
  }

function ATGet-CmsDescription
{
    param
    (
        [int] $code
    )

    $cmsError = @{}
    $cmsError.Add( 1, 'Unassigned number')
    $cmsError.Add( 8, 'Operator determined barring')
    $cmsError.Add( 10, 'Call bared')
    $cmsError.Add( 21, 'Short message transfer rejected')
    $cmsError.Add( 27, 'Destination out of service')
    $cmsError.Add( 28, 'Unindentified subscriber')
    $cmsError.Add( 29, 'Facility rejected')
    $cmsError.Add( 30, 'Unknown subscriber')
    $cmsError.Add( 38, 'Network out of order')
    $cmsError.Add( 41, 'Temporary failure')
    $cmsError.Add( 42, 'Congestion')
    $cmsError.Add( 47, 'Recources unavailable')
    $cmsError.Add( 50, 'Requested facility not subscribed')
    $cmsError.Add( 69, 'Requested facility not implemented')
    $cmsError.Add( 81, 'Invalid short message transfer reference value')
    $cmsError.Add( 95, 'Invalid message unspecified')
    $cmsError.Add( 96, 'Invalid mandatory information')
    $cmsError.Add( 97, 'Message type non existent or not implemented')
    $cmsError.Add( 98, 'Message not compatible with short message protocol')
    $cmsError.Add( 99, 'Information element non-existent or not implemente')
    $cmsError.Add( 111, 'Protocol error, unspecified')
    $cmsError.Add( 127, 'Internetworking , unspecified')
    $cmsError.Add( 128, 'Telematic internetworking not supported')
    $cmsError.Add( 129, 'Short message type 0 not supported')
    $cmsError.Add( 130, 'Cannot replace short message')
    $cmsError.Add( 143, 'Unspecified TP-PID error')
    $cmsError.Add( 144, 'Data code scheme not supported')
    $cmsError.Add( 145, 'Message class not supported')
    $cmsError.Add( 159, 'Unspecified TP-DCS error')
    $cmsError.Add( 160, 'Command cannot be actioned')
    $cmsError.Add( 161, 'Command unsupported')
    $cmsError.Add( 175, 'Unspecified TP-Command error')
    $cmsError.Add( 176, 'TPDU not supported')
    $cmsError.Add( 192, 'SC busy')
    $cmsError.Add( 193, 'No SC subscription')
    $cmsError.Add( 194, 'SC System failure')
    $cmsError.Add( 195, 'Invalid SME address')
    $cmsError.Add( 196, 'Destination SME barred')
    $cmsError.Add( 197, 'SM Rejected-Duplicate SM')
    $cmsError.Add( 198, 'TP-VPF not supported')
    $cmsError.Add( 199, 'TP-VP not supported')
    $cmsError.Add( 208, 'D0 SIM SMS Storage full')
    $cmsError.Add( 209, 'No SMS Storage capability in SIM')
    $cmsError.Add( 210, 'Error in MS')
    $cmsError.Add( 211, 'Memory capacity exceeded')
    $cmsError.Add( 212, 'Sim application toolkit busy')
    $cmsError.Add( 213, 'SIM data download error')
    $cmsError.Add( 255, 'Unspecified error cause')
    $cmsError.Add( 300, 'ME Failure')
    $cmsError.Add( 301, 'SMS service of ME reserved')
    $cmsError.Add( 302, 'Operation not allowed')
    $cmsError.Add( 303, 'Operation not supported')
    $cmsError.Add( 304, 'Invalid PDU mode parameter')
    $cmsError.Add( 305, 'Invalid Text mode parameter')
    $cmsError.Add( 310, 'SIM not inserted')
    $cmsError.Add( 311, 'SIM PIN required')
    $cmsError.Add( 312, 'PH-SIM PIN required')
    $cmsError.Add( 313, 'SIM failure')
    $cmsError.Add( 314, 'SIM busy')
    $cmsError.Add( 315, 'SIM wrong')
    $cmsError.Add( 316, 'SIM PUK required')
    $cmsError.Add( 317, 'SIM PIN2 required')
    $cmsError.Add( 318, 'SIM PUK2 required')
    $cmsError.Add( 320, 'Memory failure')
    $cmsError.Add( 321, 'Invalid memory index')
    $cmsError.Add( 322, 'Memory full')
    $cmsError.Add( 330, 'SMSC address unknown')
    $cmsError.Add( 331, 'No network service')
    $cmsError.Add( 332, 'Network timeout')
    $cmsError.Add( 340, 'No +CNMA expected')
    $cmsError.Add( 500, 'Unknown error')
    $cmsError.Add( 512, 'User abort')
    $cmsError.Add( 513, 'Unable to store')
    $cmsError.Add( 514, 'Invalid Status')
    $cmsError.Add( 515, 'Device busy or Invalid Character in string')
    $cmsError.Add( 516, 'Invalid length')
    $cmsError.Add( 517, 'Invalid character in PDU')
    $cmsError.Add( 518, 'Invalid parameter')
    $cmsError.Add( 519, 'Invalid length or character')
    $cmsError.Add( 520, 'Invalid character in text')
    $cmsError.Add( 521, 'Timer expired')
    $cmsError.Add( 522, 'Operation temporary not allowed')
    $cmsError.Add( 532, 'SIM not ready')
    $cmsError.Add( 534, 'Cell Broadcast error unknown')
    $cmsError.Add( 535, 'Protocol stack busy')
    $cmsError.Add( 538, 'Invalid parameter')

    $cmsError[$code]
}

function ATSend-Sms
{
    param
    (
        [string] $phone,
        [string] $message,
        [string] $portName
    )

    $serialPort = new-Object System.IO.Ports.SerialPort
    $serialPort.PortName = $portName
    $serialPort.BaudRate = 9600
    $serialPort.Parity = 'None'
    $serialPort.StopBits = 'One'
    $serialPort.WriteTimeout = 500
    $serialPort.ReadTimeout = 3000
    $serialPort.DtrEnable = $true

    try {
        $serialPort.Open()
        $serialPort.Write("AT+CMGF=1`r`n")
        $serialPort.Write("AT+CMGS=""$phone""`r`n")
        $serialPort.Write("$textMessage$([char] 26)`r`n")
        sleep 1
        $serialPort.ReadExisting()
        $serialPort.Close()
    } finally {
        $serialPort.Dispose();
    }
}

function ATSet-Pin
{
    param
    (
        [string] $pin,
        [string] $portName
    )

    $serialPort = new-Object System.IO.Ports.SerialPort
    $serialPort.PortName = $portName
    $serialPort.BaudRate = 19200
    $serialPort.WriteTimeout = 500
    $serialPort.ReadTimeout = 3000
    $serialPort.DtrEnable = $true

    try {
        $serialPort.Open()
        $serialPort.Write("AT+CPIN=$pin`r`n")
        sleep 1
        $serialPort.ReadExisting()
        $serialPort.Close()
    } finally {
        $serialPort.Dispose();
    }
}

function ATGet-Information
{
    param
    (
        [string] $portName
    )

    $serialPort = new-Object System.IO.Ports.SerialPort
    $serialPort.PortName = $portName
    $serialPort.BaudRate = 19200
    $serialPort.WriteTimeout = 500
    $serialPort.ReadTimeout = 3000
    $serialPort.DtrEnable = $true

    try {
        $serialPort.Open()
        $serialPort.Write("ATI`r`n")
        sleep 1
        $serialPort.ReadExisting()
        $serialPort.Close()
    } finally {
        $serialPort.Dispose();
    }
}


function ATOpen-Port
{
    param
    (
        [string] $portName
    )

    $serialPort = new-Object System.IO.Ports.SerialPort
    $serialPort.PortName = $portName
    $serialPort.BaudRate = 19200
    $serialPort.WriteTimeout = 500
    $serialPort.ReadTimeout = 3000
    $serialPort.DtrEnable = $true

    $serialPort
}

function ATGet-SignalStrength
{
    param
    (
        [string] $portName
    )

    $serialPort = new-Object System.IO.Ports.SerialPort
    $serialPort.PortName = $portName
    $serialPort.BaudRate = 19200
    $serialPort.WriteTimeout = 500
    $serialPort.ReadTimeout = 3000
    $serialPort.DtrEnable = $true

    try {
        $serialPort.Open()
        $serialPort.Write("AT+CSQ`r`n")
        sleep 1
        $serialPort.ReadExisting()
        $serialPort.Close()
    } finally {
        $serialPort.Dispose();
    }
}

function ATGet-Sms
{
    param
    (
        [string] $portName
    )

    $serialPort = new-Object System.IO.Ports.SerialPort
    $serialPort.PortName = $portName
    $serialPort.BaudRate = 19200
    $serialPort.WriteTimeout = 500
    $serialPort.ReadTimeout = 3000
    $serialPort.DtrEnable = $true

    try {
        $serialPort.Open()
        $serialPort.Write("AT+CMGL=""ALL""`r`n")
        sleep 1
        $serialPort.ReadExisting()
        $serialPort.Close()
    } finally {
        $serialPort.Dispose();
    }
}