using UnityEngine;
using System.Collections;

public class Strings : MonoBehaviour {
	// Network strings
	public const string net_err					= "Network error: ";
	public const string net_err_noconnection	= net_err + " No connection.";
	public const string net_err_hostunreacahbe	= net_err + " Host not reachable.";
	public const string net_err_checkipinput	= net_err + " Ensure you entered a valid IP address.";
	public const string net_status				= "Network status: ";
	public const string net_status_connecthost	= net_status + " Connecting to host...";
	public const string net_status_connectserver = net_status + " Connecting to server...";
}
