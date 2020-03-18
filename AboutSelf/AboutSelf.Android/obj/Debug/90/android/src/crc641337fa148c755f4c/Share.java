package crc641337fa148c755f4c;


public class Share
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("AboutSelf.Droid.Share, AboutSelf.Android", Share.class, __md_methods);
	}


	public Share ()
	{
		super ();
		if (getClass () == Share.class)
			mono.android.TypeManager.Activate ("AboutSelf.Droid.Share, AboutSelf.Android", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
