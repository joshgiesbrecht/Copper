﻿using System.IO;
using UnityEditor;
using UnityEngine;

/* 
* This script allows you to set custom icons for folders in project browser.
* Recommended icon sizes - small: 16x16 px, large: 64x64 px;
*/

namespace Ink.UnityIntegration {
	[InitializeOnLoad]
	public class InkBrowserIcons {
		private static bool isRetina {
			get {
				float unityVersion = float.Parse(Application.unityVersion.Substring (0, 3));
				return Application.platform == RuntimePlatform.OSXEditor && unityVersion >= 5.4f;
			}
		}
	    private const float largeIconSize = 64f;

		private static Texture2D _inkFileIcon;
		public static Texture2D inkFileIcon {
			get {
				if(_inkFileIcon == null) {
					if(isRetina) {
						_inkFileIcon = Resources.Load<Texture2D>("InkFileIcon-retina");
					} else {
						_inkFileIcon = Resources.Load<Texture2D>("InkFileIcon");
					}
				}
				return _inkFileIcon;
			}
		}
		private static Texture2D _errorIcon;
		public static Texture2D errorIcon {
			get {
				if(_errorIcon == null) {
					_errorIcon = Resources.Load<Texture2D>("InkErrorIcon");
				}
				return _errorIcon;
			}
		}
		private static Texture2D _warningIcon;
		public static Texture2D warningIcon {
			get {
				if(_warningIcon == null) {
					_warningIcon = Resources.Load<Texture2D>("InkWarningIcon");
				}
				return _warningIcon;
			}
		}
		private static Texture2D _childIcon;
		public static Texture2D childIcon {
			get {
				if(_childIcon == null) {
					_childIcon = Resources.Load<Texture2D>("InkChildIcon");
				}
				return _childIcon;
			}
		}
		private static Texture2D _unknownFileIcon;
		public static Texture2D unknownFileIcon {
			get {
				if(_unknownFileIcon == null) {
					_unknownFileIcon = Resources.Load<Texture2D>("InkUnknownFileIcon");
				}
				return _unknownFileIcon;
			}
		}

	    static InkBrowserIcons() {
			EditorApplication.projectWindowItemOnGUI += OnDrawProjectWindowItem;
	    }

	    static void OnDrawProjectWindowItem(string guid, Rect rect) {
	    	if(!InkLibrary.created)
	    		return;
	        var path = AssetDatabase.GUIDToAssetPath(guid);

			if (Path.GetExtension(path) == InkEditorUtils.inkFileExtension) {
				InkFile inkFile = InkLibrary.GetInkFileWithPath(path);

				var isSmall = rect.width > rect.height;
				if (isSmall) {
					rect.width = rect.height;
				} else {
					rect.height = rect.width;
				}

				if (rect.width > largeIconSize) {
					var offset = (rect.width - largeIconSize) * 0.5f;
					var position = new Rect(rect.x + offset, rect.y + offset, largeIconSize, largeIconSize);
					if(inkFileIcon != null)
						GUI.DrawTexture(position, inkFileIcon);
				}
				else {
					if(inkFileIcon != null)
						GUI.DrawTexture(rect, inkFileIcon);

					if(inkFile == null) {
						if(unknownFileIcon != null) {
							GUI.DrawTexture(new Rect(rect.x, rect.y, unknownFileIcon.width, unknownFileIcon.height), unknownFileIcon);
						}
					} else {
						Rect miniRect = new Rect(rect.center, rect.size * 0.5f);
						if(inkFile.hasErrors && errorIcon != null) {
							GUI.DrawTexture(miniRect, errorIcon);
						} else if(inkFile.hasWarnings && warningIcon != null) {
							GUI.DrawTexture(miniRect, warningIcon);
						}
						if(!inkFile.isMaster && childIcon != null) {
							GUI.DrawTexture(new Rect(rect.x, rect.y, childIcon.width, childIcon.height), childIcon);
						}
					}
				}
			}
	    }
	}
}