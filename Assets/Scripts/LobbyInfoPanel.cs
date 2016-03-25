using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CustomLobbyNetwork {
	public class LobbyInfoPanel : MonoBehaviour {
		public Text 	m_infoText;
		public Text		m_buttonText;
		public Button	m_button;

		public void Display( string info, string buttonInfo, UnityEngine.Events.UnityAction buttonClbk ) {
			m_infoText.text = info;

			m_buttonText.text = buttonInfo;

			m_button.onClick.RemoveAllListeners();

			if ( buttonClbk != null ) 
				m_button.onClick.AddListener( buttonClbk );

			m_button.onClick.AddListener( () => { gameObject.SetActive( false ); });

			gameObject.SetActive( true );
		}
	}
}