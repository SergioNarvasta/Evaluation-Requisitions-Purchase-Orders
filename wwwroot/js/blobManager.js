

const blobToBase64 = (blob) => {
    return new Promise( (resolve, reject) =>{
        const reader = new FileReader();
        reader.readAsDataURL(blob);
        reader.onloadend = () => {
            resolve(reader.result.split(',')[1]);
            // "data:image/jpg;base64,    =sdCXDSAsadsadsa"
        };
    });
};


const b64ToBlob = async(b64, type)=>{
    const blob = await fetch(`data:${type};base64,${b64}`);
    return blob;
};


export{
    blobToBase64,
    b64ToBlob
}