using System;

namespace engine
{
enum DType
{
    dobj=0,
    ddict,
    darray,
    dmatrix,
    dvolume,
    dstruct,
}

enum ObjType
{
    objnone = 0,
    objdict,
    objarray,
    objmatrix,
    objvolume,
    objstruct,
    objstr,
    objbool,
    objsint8,
    objsint16,
    objsint32,
    objsint64,
    objuint8,
    objuint16,
    objuint32,
    objuint64,
    objreal32,
    objreal64,
    objcomplex32,
    objcomplex64,

}

public class DObject
{
    DType dType;
    ObjType objType;
    dynamic obj = System.Dynamic.ExpandoObject();

    public DObject(DType dType, ObjType objType)
    {
        this.dType = dType;
        if(this.dType == DType.ddict)
        {
            this.obj.__dict__ = new Dictionary<DObject, DObject>();
            this.objType = ObjType.objdict;
        }

        else if(this.dType == DType.darray)
        {
            this.obj.__array__ = new List<DObject>();
            this.objType = ObjType.objarray;
        }

        else if(this.dType==DType.dmatrix)
        {
            this.obj.__matrix__ = new List<List<DObject>>();
            this.objType = ObjType.objmatrix;
        }
        else if(this.dType==DType.dvolume)
        {
            this.obj.__volume__ =  new List<List<list<DObject>>>();
            this.objType = ObjType.objvolume;
        }
        else if(this.dType==DType.dstruct)
        {
            this.obj.__header__ = new Dictionary<string, ObjType>();
            this.obj.__field__ = new Dictionary<string, DObject>(); 
        }
        else if (this.dType == DType.dobj)
        {
            this.objType = objType;
            this.obj.__value__ =  null;
        }
    }

    public static DObject operator+(DObject self, DObject other)
    {
        if(self.dType != other.dType)
        {
            DObject dObject = DObjectFactory.buildObj(DType.dobj, ObjType.objnone);
            return dObject;
        }
        else
        {
            if(self.dType == DType.dobj)
            {
            }
        }
    }

    public void addField(string name, DObject dObject)
    {
        if(this.dType == DType.dstruct)
        {
            this.obj.__field__[name] = dObject;
        }
    }

    public void delField(string name)
    {
        if(this.dType == DType.dstruct)
        {
            this.obj.__field__.remove(name);
        }
    }

    public void setValue(dynamic value)
    {
        if(this.dType == DType.dobj)
        {
            this.obj.__value__ = value;
        }
    }

    public dynamic getValue()
    {
        if(this.dType == DType.dobj)
        {
            return this.obj.__value__;
        }
        else
        {
            dynamic r = null;
            return r;
        }
    }

    public DObject getField(string name)
    {
        if(this.dType == DType.dstruct)
        {
            return this.obj.__field__[name];
        }
        else
        {
            DObject dObject = new DObject();
            return dObject;
        }
    }

    public void setField(ObjType objType, string fieldName, dynamic data)
    {
        if(this.dType == DType.dstruct)
        {
            this.obj.__header__[fieldName] = objType;
            this.obj.__field__[fieldName] = data;
        }
    }

}

public static class DObjectFactory
{
    public DObject buildObj(DType dType, ObjType objType)
    {
        DObject dObject = new DObject(dType);
        
        if(dType == DType.dobj)
        {

            switch(objType)
            {
                case ObjType.objbool:
                dObject.__value__ = false;

                case ObjType.objstr:
                dObject.__value__ = "";

                case ObjType.objcomplex32:
                dObject.__value__ = (0.0f, 0.0f);

                case ObjType.objcomplex64:
                dObject.__value__ = (0.0d, 0.0d);

                default:
                dObject.__value__ = 0;
            }
        }
        return dObject;
    }
}

}