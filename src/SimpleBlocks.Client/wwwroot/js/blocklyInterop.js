window.BlocklyInterop = {
    workspace: null,

    clearBlockly: function () {
        const div = document.getElementById('blocklyDiv');
        if (div) {
            div.innerHTML = '';
        }
    },
    
    initBlockly: async function(blocksJson) {
        try {
            const config = JSON.parse(blocksJson);
            
            if (Array.isArray(config.blocks)) {
                Blockly.defineBlocksWithJsonArray(config.blocks);
            }

            const toolboxXml = config.toolbox?.join('\n') || '<xml></xml>';
            this.workspace = Blockly.inject('blocklyDiv', {
                toolbox: toolboxXml,
                trashcan: true,
                media: 'https://unpkg.com/blockly/media/',
                sounds: false
            });

            setTimeout(() => Blockly.svgResize(this.workspace), 100);
        } catch (error) {
            console.error('[ERROR] Blockly init failed:', error);
            alert('Ошибка инициализации: ' + error.message);
        }
    },

    getWorkspaceJson: function() {
        function flattenBlocks(blocks) {
            const result = [];
            function processBlock(block) {
                if (!block) return;
                const { next, ...cleanBlock } = block;
                result.push(cleanBlock);
                if (next) processBlock(next);
            }
            blocks.forEach(block => processBlock(block));
            return result;
        }
        
        try {
            const xmlString = this.getWorkspaceXml();
            if (!xmlString) return null;

            const parser = new DOMParser();
            const xmlDoc = parser.parseFromString(xmlString, "text/xml");

            const parseBlock = (blockNode) => {
                const originalType = blockNode.getAttribute('type');

                const block = {
                    name: originalType,
                    inputFields: {},
                    body: []
                };

                // Поля (fields)
                blockNode.querySelectorAll(':scope > field').forEach(field => {
                    const fieldName = this.toCamelCase(field.getAttribute('name'));
                    block.inputFields[fieldName] = field.textContent;
                });

                // Вложенные значения (values)
                blockNode.querySelectorAll(':scope > value').forEach(input => {
                    const inputName = this.toCamelCase(input.getAttribute('name'));
                    const firstChildBlock = input.querySelector('block');
                    if (firstChildBlock) {
                        block.inputFields[inputName] = parseBlock(firstChildBlock);
                    }
                });

                // Тело (statements)
                blockNode.querySelectorAll(':scope > statement').forEach(statement => {
                    const statementName = this.toCamelCase(statement.getAttribute('name'));
                    let currentBlock = statement.querySelector('block');
                    const statementBlocks = [];
                    while (currentBlock) {
                        statementBlocks.push(parseBlock(currentBlock));
                        currentBlock = currentBlock.querySelector(':scope > next > block');
                    }
                    block.body = statementBlocks;
                });

                // Next-блоки (отдельная цепочка)
                let nextBlock = blockNode.querySelector(':scope > next > block');
                if (nextBlock) {
                    block.next = parseBlock(nextBlock);
                }

                return this.cleanEmptyProperties(block);
            };

            const rootBlocks = Array.from(xmlDoc.documentElement.children)
                .filter(x => x.tagName === 'block')
                .map(parseBlock);

            const flatBlocks = flattenBlocks(rootBlocks);
            return JSON.stringify(flatBlocks, null, 2);
        } catch (e) {
            console.error("JSON conversion error:", e);
            return JSON.stringify({ error: e.message });
        }
    },

    // Хелперы
    toCamelCase: function(str) {
        return str.replace(/(_\w)/g, m => m[1].toUpperCase());
    },

    cleanEmptyProperties: function(obj) {
        return Object.entries(obj).reduce((acc, [key, value]) => {
            if (Array.isArray(value) && value.length === 0) return acc;
            if (value && typeof value === 'object' && Object.keys(value).length === 0) return acc;
            acc[key] = value;
            return acc;
        }, {});
    },

    getWorkspaceXml: function() {
        if (!this.workspace) return null;
        const xml = Blockly.Xml.workspaceToDom(this.workspace);
        return Blockly.Xml.domToText(xml);
    }
};