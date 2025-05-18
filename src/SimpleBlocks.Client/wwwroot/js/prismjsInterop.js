window.setCodeAndHighlight = async (element, code, language) => {
    if (!element) return;
    element.textContent = code;

    try {
        await Prism.plugins.autoloader.loadLanguages(language);
        element.className = `language-${language}`;
        Prism.highlightElement(element);
    } catch {
        element.className = 'language-plaintext';
        Prism.highlightElement(element);
    }
};