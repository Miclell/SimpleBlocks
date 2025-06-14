export function readFileAsText(inputRef) {
    return new Promise((resolve, reject) => {
        const file = inputRef.files[0];
        if (!file) {
            resolve(null);
            return;
        }

        const reader = new FileReader();
        reader.onload = (e) => resolve(e.target.result);
        reader.onerror = (e) => reject(e);
        reader.readAsText(file);
    });
} 