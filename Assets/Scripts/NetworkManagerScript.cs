using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {

	public float panelSizeMinification;

	NetworkManager m_networkMgr;
	NetworkManagerHUD m_networkMgrHud;

	public RectTransform m_networkPanel;

	void Awake () {
		// Test code
		if (!m_networkMgr)
			m_networkMgr = GetComponent<NetworkManager>();

		// Network Manager HUD initialization
		m_networkMgrHud = GetComponent<NetworkManagerHUD>();
		m_networkMgrHud.manager = m_networkMgr;

		// Network panel UI
		int panelSize_x = (int)(Screen.width * panelSizeMinification);
		int panelSize_y = (int)(Screen.height * panelSizeMinification);
		m_networkPanel.sizeDelta = new Vector2(panelSize_x, panelSize_y);
		m_networkPanel.localPosition = new Vector3(0, 0, 0);
	}
}
